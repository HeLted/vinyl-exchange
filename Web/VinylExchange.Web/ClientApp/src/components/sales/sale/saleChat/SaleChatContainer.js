import React, { Component } from "react";
import SaleChatComponent from "./SaleChatComponent";
import * as SignalR from "@microsoft/signalr";
import { NotificationContext } from "./../../../../contexts/NotificationContext";
import { Url, Controllers } from "./../../../../constants/UrlConstants";
import axios from "axios";

class SaleChatContainer extends Component {
  constructor(props) {
    super(props);
    this.state = {
      messageInput: "",
      messages: [],
      avatars: [],
      isLoading: false
    };
    this.connection = null;
  }

  static contextType = NotificationContext;

  componentDidMount() {
    this.setState({ isLoading: true });

    this.connection = new SignalR.HubConnectionBuilder()
      .withUrl("/Sale/ChatHub")
      .build();

    this.connection.on("NewMessage", this.recieveMessage);
    this.connection.on("LoadMessageHistory", this.loadMessageHistory);

    this.connection
      .start()
      .then(() => {
        this.context.handleAppNotification(
          "Established connection with Chat Hub",
          5
        );
        this.joinRoom().then(() => {
          this.getSellerAvatarPromise().then(() => {
            const buyerAvatarPromise = this.getBuyerAvatarPromise();

            if (buyerAvatarPromise != undefined) {
              buyerAvatarPromise.then(() => {
                this.invokeLoadMessageHistory();
              });
            } else {
              this.invokeLoadMessageHistory();
            }
          });
        });
      })
      .catch(error => {
        this.setState({ isLoading: false });
        this.context.handleAppNotification(
          "There was an error in establishing connection with chatHub!",
          1
        );
      });
  }

  joinRoom = () => {
    return this.connection
      .invoke("JoinRoom", this.props.data.sale.id)
      .then(() => {
        this.context.handleAppNotification("You joined sale chatroom!", 3);
      })
      .catch(err => {
        this.setState({ isLoading: false });
        this.context.handleAppNotification(
          "There was an error with joining chat room!",
          1
        );
      });
  };

  getSellerAvatarPromise = () => {
    return axios
      .get(
        Url.api +
          Controllers.users.name +
          Controllers.users.actions.getUserAvatar +
          Url.slash +
          this.props.data.sale.sellerId
      )
      .then(response => {
        this.context.handleAppNotification("Loaded seller avatar to chat", 5);
        this.setState(prevState => {
          const updatedAvatars = prevState.avatars;
          updatedAvatars.push({
            userId: this.props.data.sale.sellerId,
            avatar: response.data.avatar
          });
          return { avatars: updatedAvatars };
        });
      })
      .catch(error => {
        this.context.handleServerNotification(
          error.response,
          "Failed to load seller avatar to chat!"
        );
      });
  };

  getBuyerAvatarPromise = () => {
    if (this.props.data.sale.buyerId !== null) {
      return axios
        .get(
          Url.api +
            Controllers.users.name +
            Controllers.users.actions.getUserAvatar +
            Url.slash +
            this.props.data.sale.buyerId
        )
        .then(response => {
          this.context.handleAppNotification("Loaded buyer avatar to chat", 5);
          this.setState(prevState => {
            const updatedAvatars = prevState.avatars;
            updatedAvatars.push({
              userId: this.props.data.sale.buyerId,
              avatar: response.data.avatar
            });
            return { avatars: updatedAvatars, isLoading: false };
          });
        })
        .catch(error => {
          this.setState({ isLoading: false });
          this.context.handleServerNotification(
            error.response,
            "Failed to load buyer avatar to chat!"
          );
        });
    }
  };

  handleSendMessage = () => {
    this.connection
      .invoke("SendMessage", this.props.data.sale.id, this.state.messageInput)
      .catch(err => {
        this.context.handleAppNotification(
          "There was an error with sending message to chat room!",
          1
        );
      });
  };

  invokeLoadMessageHistory = () => {
    this.connection
      .invoke("LoadMessageHistory", this.props.data.sale.id)
      .catch(error => {
        this.context.handleAppNotification(
          "Failed to load chat message history!",
          1
        );
      });
  };

  loadMessageHistory = messages => {
    messages.forEach(messageObj => this.recieveMessage(messageObj));
  };

  recieveMessage = messageObj => {


      this.setState(prevState => {
        const updatedMesssages = prevState.messages;
        updatedMesssages.push({
          user: messageObj.userId,
          message: messageObj.content,
    
  
          avatar: this.state.avatars[
            this.state.avatars
              .map(avatarObj => avatarObj.userId)
              .indexOf(messageObj.userId)
          ].avatar
        });
        return { messages: updatedMesssages };
      });
    
   
  }


  handleOnChange = event => {
    const { value, name } = event.target;
    this.setState({ [name]: value });
  };

  handleToggleChat = () => {
    this.setState(prevState => {
      return { isChatShown: prevState.isChatShown ? false : true };
    });
  };

  componentWillUnmount() {
    this.connection.stop();
  }

  render() {
    return (
      <SaleChatComponent
        data={{
          messageInput: this.state.messageInput,
          messages: this.state.messages,
          isLoading: this.state.isLoading,
          isChatShown : this.state.isChatShown
        }}
        functions={{
          handleSendMessage: this.handleSendMessage,
          handleOnChange: this.handleOnChange,
          handleToggleChat: this.handleToggleChat
        }}
      />
    );
  }
}

export default SaleChatContainer;

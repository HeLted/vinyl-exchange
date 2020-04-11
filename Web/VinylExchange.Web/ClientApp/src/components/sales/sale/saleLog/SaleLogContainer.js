import React, { Component } from "react";
import SaleLogComponent from "./SaleLogComponent";
import * as SignalR from "@microsoft/signalr";
import { NotificationContext } from "./../../../../contexts/NotificationContext";
import { Url, Controllers } from "./../../../../constants/UrlConstants";
import axios from "axios";

class SaleLogContainer extends Component {
  constructor(props) {
    super(props);
    this.state = {
      logs: [],
      isLoading:false
    };
    this.connection = null;
  }

  static contextType = NotificationContext;

  componentDidMount() {
    this.setState({ isLoading: true });

    this.connection = new SignalR.HubConnectionBuilder()
      .withUrl("/Sale/LogHub")
      .build();

    this.connection.on("RecieveLogNotification", this.recieveLogNotification);
    this.connection.on("LoadLogHistory", this.loadLogHistory);
  
    this.connection
      .start()
      .then(() => {
        this.context.handleAppNotification(
          "Established connection with Log Hub",
          5
        );
        this.joinRoom().then(()=>{
          this.connection.invoke("LoadLogHistory",this.props.data.sale.id)
          .then(()=>{
            this.context.handleAppNotification("Loaded log history",5);
            this.setState({ isLoading: false });
          })
          .catch(()=>{
            this.context.handleAppNotification("Failed to load log history");
             this.setState({ isLoading: false });
          })
        });
      })
      .catch(error => {
        this.context.handleAppNotification(
          "Failed to establish connection with Log Hub",
          1
        );
         this.setState({ isLoading: false });
      });
  }

  joinRoom = () => {
    return this.connection
      .invoke("SubscribeToLog", this.props.data.sale.id)
      .then(() => {
        this.context.handleAppNotification("Subscribed to sale log!", 5);
      })
      .catch(err => {
        this.setState({ isLoading: false });
        this.context.handleAppNotification(
          "There was an error with subscribing to sale log!",
          1
        );
      });
  };


  loadLogHistory = logs =>{
    const logHistory = logs.map(logObj=> {
      return {
        id: logObj.id,
        createdOn: logObj.createdOn,
        logContent: logObj.content
      }
    })
   
    this.setState({logs:logHistory});

  }

  recieveLogNotification = notificationContent => {

    this.context.handleAppNotification(notificationContent,6);
    
    this.props.functions.handleReLoadSale();
    
  };

  componentWillUnmount(){
    this.connection.stop();
  }

  render() {
    return <SaleLogComponent data={{logs:this.state.logs,isLoading:this.state.isLoading}}/>;
  }
}

export default SaleLogContainer;

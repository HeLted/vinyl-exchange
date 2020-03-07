import React, { Component } from "react";
import { Url, Controllers } from "./../../constants/UrlConstants";
import axios from "axios";
import { NotificationContext } from "./../../contexts/NotificationContext";

class UserThubnail extends Component {
  constructor() {
    super();
    this.state = {
      avatar: "",
      userId: "",
      isIdChosen: false
    };
  }

  static contextType = NotificationContext;

  componentDidMount() {
    if (this.props.data.avatar != undefined) {
      this.setState({ avatar: this.props.data.avatar, isIdChosen: true });
    }

    if (
      this.props.data.userId !== "" &&
      this.state.isIdChosen === false &&
      this.props.data.avatar == undefined
    ) {
      this.setState({ isIdChosen: true });
      this.loadAvatar(this.props.data.userId);
    }
  }

  loadAvatar = userId => {
    this.setState({ isLoading: true });

    axios
      .get(
        Url.api +
          Controllers.users.name +
          Controllers.users.actions.getUserAvatar +
          Url.slash +
          userId
      )
      .then(response => {
        this.context.handleAppNotification("Loaded user avatar", 5);
        this.setState({ avatar: response.data.avatar });
      })
      .catch(error => {
        this.context.handleServerNotification(
          error.response,
          "Failed to load user avatar!"
        );
      });
  };

  render() {
    return this.state.avatar.length === 0 ? (
      <div className="spinner-border text-primary" role="status">
        <span className="sr-only">Loading...</span>
      </div>
    ) : (
      <img
        className="thumbnail-image img-fluid"
        height={
          (this.props.data.height != undefined ? this.props.data.height : 50) +
          "px"
        }
        width={
          (this.props.data.width != undefined ? this.props.data.width : 50) +
          "px"
        }
        src={"data:image/png;base64, " + this.state.avatar}
      ></img>
    );
  }
}

export default UserThubnail;

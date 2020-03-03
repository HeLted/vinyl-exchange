import React, { Component } from "react";
import BorderSpinner from "./spinners/BorderSpinner";
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

  componentWillReceiveProps(nextProps) {
    if (
      nextProps.data.userId !== "" &&
      nextProps.data.userId !== null &&
      this.state.isIdChosen === false
    ) {
      this.setState({ userId: nextProps.data.userId, isIdChosen: true });
      this.loadAvatar(nextProps.data.userId);
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
        className="thumbnail-image"
        height="50px"
        width="50px"
        src={"data:image/png;base64, " + this.state.avatar}
      ></img>
    );
  }
}

export default UserThubnail;

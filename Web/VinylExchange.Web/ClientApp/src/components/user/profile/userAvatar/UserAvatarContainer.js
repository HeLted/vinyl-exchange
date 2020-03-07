import React, { Component } from "react";
import UserAvatarComponent from "./UserAvatarComponent";
import axios from "axios";
import {
  Url,
  Controllers,
  Queries
} from "./../../../../constants/UrlConstants";
import { NotificationContext } from "./../../../../contexts/NotificationContext";

class UserAvatarContainer extends Component {
  constructor(props) {
    super(props);
    this.state = {
      avatar: "",
      isLoading: false
    };
  }

  static contextType = NotificationContext;

  componentDidMount() {
    this.loadAvatar();
  }

  componentWillReceiveProps(nextProps) {
    if (
      this.props.data.shouldAvatarUpdate !== nextProps.data.shouldAvatarUpdate
    ) {
      this.loadAvatar();
    }
  }

  loadAvatar = () => {
    this.setState({ isLoading: true });

    axios
      .get(
        Url.api +
          Controllers.users.name +
          Controllers.users.actions.getCurrentUserAvatar
      )
      .then(response => {
        this.context.handleAppNotification("Loaded user avatar", 5);
        this.setState({ avatar: response.data.avatar, isLoading: false });
      })
      .catch(error => {
        this.setState({ isLoading: false });
        this.context.handleAppNotification(
          error.response,
          "Failed to load user avatar!"
        );
      });
  };

  render() {
    return (
      <UserAvatarComponent
        data={{ avatar: this.state.avatar, isLoading: this.state.isLoading }}
      />
    );
  }
}

export default UserAvatarContainer;

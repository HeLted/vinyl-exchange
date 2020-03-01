import React, { Component } from "react";
import UserAvatarComponent from "./UserAvatarComponent";
import axios from "axios";
import {
  Url,
  Controllers,
  Queries
} from "./../../../../constants/UrlConstants";

class UserAvatarContainer extends Component {
  constructor(props) {
    super(props);
    this.state = {
      avatar: "",
      isLoading: false
    };
  }

  componentDidMount() {
    this.loadAvatar();
  }

  componentWillReceiveProps(){
    this.loadAvatar();
  }

  loadAvatar = () =>{
    this.setState({ isLoading: true });

    axios
      .get(
        Url.authentication +
          Controllers.users.name +
          Controllers.users.actions.getCurrentUserAvatar
      )
      .then(response => {
        this.setState({ avatar: response.data.avatar, isLoading: false });
      })
      .catch(error => {});
  }

  render() {
    return (
      <UserAvatarComponent
        data={{ avatar: this.state.avatar, isLoading: this.state.isLoading }}
      />
    );
  }
}

export default UserAvatarContainer;

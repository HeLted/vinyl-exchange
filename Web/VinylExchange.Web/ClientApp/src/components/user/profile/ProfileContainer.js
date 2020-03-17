import React, { Component } from "react";
import ProfileComponent from "./ProfileComponent";
import authService from "./../../api-authorization/AuthorizeService";

class ProfileContainer extends Component {
  constructor(props) {
    super(props);
    this.state = {
      user: null,
      isLoading: true,
      shouldAvatarUpdate: false
    };
  }

  componentDidMount() {
    authService.getUser().then(userObj => {
      this.setState({ user: userObj, isLoading: false });
    });
  }

  handleShouldAvatarUpdate = () => {
    this.setState(prevState => {
      return {
        shouldAvatarUpdate: prevState.shouldAvatarUpdate ? false : true
      };
    });
  };

  render() {
    return (
      <ProfileComponent
        functions={{ handleShouldAvatarUpdate: this.handleShouldAvatarUpdate }}
        data={{
          shouldAvatarUpdate: this.state.shouldAvatarUpdate,
          isLoading: this.state.isLoading,
          user: this.state.user
        }}
      />
    );
  }
}

export default ProfileContainer;

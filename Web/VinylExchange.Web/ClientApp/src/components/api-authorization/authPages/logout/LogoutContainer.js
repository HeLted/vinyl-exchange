import React, { Component } from "react";
import authService from "./../../../api-authorization/AuthorizeService";
import PageSpinner from "./../../../common/spinners/PageSpinner";

class LogoutContainer extends Component {
  constructor(props) {
    super(props);
    this.state = {};
  }

  componentDidMount() {
    this.LogOut();
  }

  LogOut = async () => {
    const isAuthenticated = await authService.isAuthenticated();

    if (isAuthenticated) {
      await authService.signOut({data:"/"});
      await authService.completeSignOut("/");

    }
  };

  render() {
    return <PageSpinner/>;
  }
}

export default LogoutContainer;

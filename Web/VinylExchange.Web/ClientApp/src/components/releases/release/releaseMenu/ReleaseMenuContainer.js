import React, { Component } from "react";
import ReleaseMenuComponent from "./ReleaseMenuComponent";
import authService from "./../../../api-authorization/AuthorizeService";

class ReleaseMenuContainer extends Component {
  constructor(props) {
    super(props);
    this.state = {
      user: null,
      isLoading: true
    };
  }

  componentDidMount() {
    authService.getUser().then(userObj => {
      this.setState({ user: userObj, isLoading: false });
    });
  }

  render() {
    return (
      <ReleaseMenuComponent
        data={{
          user: this.state.user,
          releaseId: this.props.data.releaseId,
          isLoading: this.state.isLoading
        }}
      />
    );
  }
}

export default ReleaseMenuContainer;

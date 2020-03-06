import React, { Component } from "react";
import PageSpinner from "./../../../common/spinners/PageSpinner";

class LogoutCallbackContainer extends Component {

  componentDidMount() {
   this.props.history.push("/")
  }

  render() {
    return < PageSpinner/>;
  }
}

export default LogoutCallbackContainer;

import React, { Component } from "react";
import ReleaseMenuComponent from "./ReleaseMenuComponent";

class ReleaseMenuContainer extends Component {
  render() {
   return (<ReleaseMenuComponent data={{ releaseId: this.props.data.releaseId}} />);
  }
}

export default ReleaseMenuContainer;

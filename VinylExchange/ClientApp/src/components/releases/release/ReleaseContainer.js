import React,{Component} from "react";
import ReleaseComponent from "./ReleaseComponent";
import urlPathIdSeparator from "./../../../functions/urlPathIdSeparator";

class ReleaseContainer extends Component {

  render() {
    return (
      <ReleaseComponent
        data={{ releaseId:urlPathIdSeparator(this.props.location.pathname)}}
      />
    );
  }
}

export default ReleaseContainer;
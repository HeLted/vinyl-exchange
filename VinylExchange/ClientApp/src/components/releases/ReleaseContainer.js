import React,{Component} from "react";
import ReleaseComponent from "./ReleaseComponent";

class ReleaseContainer extends Component {
  render() {
    return (
      <ReleaseComponent
        data={{ releaseId: this.props.location.state.releaseId }}
      />
    );
  }
}

export default ReleaseContainer;

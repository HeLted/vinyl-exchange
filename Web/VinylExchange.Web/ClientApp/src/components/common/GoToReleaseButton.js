import React, { Component } from "react";
import { withRouter } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faArrowRight } from "@fortawesome/free-solid-svg-icons";

class GoToReleaseButton extends Component {
  constructor(props) {
    super(props);
  }

  handleToRelease = () => {
    this.props.history.push(`/release/${this.props.data.releaseId}`, {
      releaseId: this.props.data.releaseId
    });
  };

  render() {
    return (
      <button type="button"  className="btn-spr btn btn-primary" onClick={()=> this.handleToRelease()}>
        <FontAwesomeIcon icon={faArrowRight} />
      </button>
    );
  }
}

export default withRouter(GoToReleaseButton);

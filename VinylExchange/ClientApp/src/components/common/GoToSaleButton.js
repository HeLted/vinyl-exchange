import React, { Component } from "react";
import { withRouter } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faArrowRight } from "@fortawesome/free-solid-svg-icons";

class GoToSaleButton extends Component {
  constructor(props) {
    super(props);
  }

  handleToRelease = () => {
    this.props.history.push(`/sale/${this.props.data.saleId}`, {
      saleId: this.props.data.saleId
    });
  };

  render() {
    return (
      <button type="button"  className="btn btn-primary" onClick={()=> this.handleToRelease()}>
        <FontAwesomeIcon icon={faArrowRight} />
      </button>
    );
  }
}

export default withRouter(GoToSaleButton);
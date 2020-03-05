import React, { Component } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEuroSign } from "@fortawesome/free-solid-svg-icons";

class PriceTagBadge extends Component {
  render() {
    return (
      <span class="tag">
        {this.props.data.price === 0 ?"N/A":(this.props.data.price )} <FontAwesomeIcon icon={faEuroSign} />
      </span>
    );
  }
}

export default PriceTagBadge;

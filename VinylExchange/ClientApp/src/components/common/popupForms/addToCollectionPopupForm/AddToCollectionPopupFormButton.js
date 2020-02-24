import React, { Component } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faHeart } from "@fortawesome/free-solid-svg-icons";
import { NotificationContext } from "./../../../../contexts/NotificationContext";

class AddToCollectionPopupFormButton extends Component {
 
  static contextType = NotificationContext;
 
  notifyIfReleaseAlreadyInUserCollection = () => {
    if (this.props.data.isReleaseAlreadyInUserCollection) {
      this.context.handleAppNotification("Release already in collection!",2)
    }
  };

  render() {
    const buttonClass = this.props.data.isReleaseAlreadyInUserCollection
      ? "btn btn-outline-danger"
      : "btn btn-outline-primary";

    return (
      <button
        type="button"
        className={buttonClass}
        data-toggle="modal"
        onClick={this.notifyIfReleaseAlreadyInUserCollection}
        data-target="#addToCollectionModal"
      >
        <FontAwesomeIcon icon={faHeart} />
      </button>
    );
  }
}

export default AddToCollectionPopupFormButton;

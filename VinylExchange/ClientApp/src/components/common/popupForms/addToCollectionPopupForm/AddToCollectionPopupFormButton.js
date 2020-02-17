import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faHeart } from "@fortawesome/free-solid-svg-icons";

function AddToCollectionPopupFormButton(props) {
  const buttonClass = props.data.isReleaseAlreadyInCollection
    ? "btn btn-outline-danger"
    : "btn btn-outline-primary";

  return (
    <button
      type="button"
      className= {buttonClass}
      data-toggle="modal"
      data-target="#exampleModalCenter"
    >
      <FontAwesomeIcon icon={faHeart} />
    </button>
  );
}

export default AddToCollectionPopupFormButton;

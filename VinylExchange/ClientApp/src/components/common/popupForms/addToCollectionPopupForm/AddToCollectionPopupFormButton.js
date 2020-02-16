import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faHeart } from "@fortawesome/free-solid-svg-icons";


function AddToCollectionPopupFormButton (){

return (<button
    type="button"
    class="btn btn-outline-primary"
    data-toggle="modal"
    data-target="#exampleModalCenter"
  >
    <FontAwesomeIcon icon={faHeart} />
  </button>)
}

export default AddToCollectionPopupFormButton;
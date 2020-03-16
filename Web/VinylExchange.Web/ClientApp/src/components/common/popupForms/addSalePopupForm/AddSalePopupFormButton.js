import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faMoneyCheck } from "@fortawesome/free-solid-svg-icons";

function AddSalePopupFormButton(props) {
  return (
    <button
      type="button"
      className="btn-spr btn btn-outline-success"
      data-toggle="modal"
      onClick={() => {
        props.functions.handleLoadColletionItemData(
          props.data.collectionItemId
        );
        props.functions.handleLoadUserAddresses();
      }}
      data-target={"#addSaleModal" + props.data.collectionItemId}
    >
      <FontAwesomeIcon icon={faMoneyCheck} />
    </button>
  );
}

export default AddSalePopupFormButton;

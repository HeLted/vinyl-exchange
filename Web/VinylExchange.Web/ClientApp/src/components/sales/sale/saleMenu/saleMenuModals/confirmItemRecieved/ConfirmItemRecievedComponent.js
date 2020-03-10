import React from "react";
import ConfirmItemSentContainer from "../confirmItemSent/ConfirmItemSentContainer";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSync } from "@fortawesome/free-solid-svg-icons";


function ConfirmItemRecievedContainer(props) {
  return (
    <div
      className="modal"
      id="confirmItemRecievedModal"
      tabIndex="-1"
      role="dialog"
    >
      <div className="modal-dialog" role="document">
        <div className="modal-content text-center">
          <div className="modal-header text-center">
            <h5 className="property-text-nm modal-title">
              Confirm Item Recieved{" "}
              {props.data.isSubmitLoading && (
                <FontAwesomeIcon icon={faSync} spin />
              )}
            </h5>
          </div>
          <br />
          <div className="address-modal-body modal-body">
            <div className="row text-center justify-content-center">
              <h5 className="property-text-nm modal-title">
                Do you confirm that you recieved the desired item ?
              </h5>
            </div>
            <div className="row justify-content-center">
              <button
                type="button"
                className="prompt-btn btn btn-success btn-lg"
                onClick={props.functions.handleOnSubmit}
              >
                Yes
              </button>
              <button
                type="button"
                className="prompt-btn btn btn-danger btn-lg"
                data-dismiss="modal"
              >
                No
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default ConfirmItemRecievedContainer;

import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSync } from "@fortawesome/free-solid-svg-icons";


function ConfirmEmailModalComponent(props) {
  return (
    <div className="modal" id="confirmEmailModal" tabIndex="-1" role="dialog">
      <div className="modal-dialog" role="document">
        <div className="modal-content text-center">
          <div className="modal-header">
            <h5 className="property-text-nm modal-title">Confirm Email</h5>
          </div>
          <div className="address-modal-body modal-body">
            <div className="row justify-content-center">
              {props.data.isEmailSend && (
                
                <div className="col-12 border" style={{margin:"20px"}}> 
                  <h5 className="property-text">
                    Confirmation email was sent to your email address!
                  </h5>
                     
                </div>
             
         
              )}
             
              {props.data.isLoading ? (
                <button type="button" className="btn btn-success">
                  <FontAwesomeIcon icon={faSync} spin />
                </button>
              ) : (
                <button
                  type="button"
                  className="btn btn-success"
                  onClick={props.functions.handleOnSubmit}
                >
                  {props.data.isEmailSend
                    ? "Re-Send Confirmation Email"
                    : "Send Confirmation Email"}
                </button>
              )}
            </div>
          </div>
          <div className="modal-footer">
            <button
              type="button"
              className="btn btn-secondary"
              data-dismiss="modal"
            >
              Close
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}

export default ConfirmEmailModalComponent;

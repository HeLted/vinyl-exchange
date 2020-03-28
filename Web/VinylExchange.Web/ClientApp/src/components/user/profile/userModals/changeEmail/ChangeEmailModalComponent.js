import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSync } from "@fortawesome/free-solid-svg-icons";
import Label from "./../../../../common/inputComponents/Label";
import TextInput from "./../../../../common/inputComponents/TextInput";

function ChangeEmailModalComponent(props) {
  return (
    <div className="modal" id="changeEmailModal" tabIndex="-1" role="dialog">
      <div className="modal-dialog" role="document">
        <div className="modal-content text-center">
          <div className="modal-header">
            <h5 className="property-text-nm modal-title">Change Email</h5>
          </div>
          <div className="address-modal-body modal-body">
            <div className="row justify-content-center">
              <div className="form-group">
                <Label for="newEmailInput" value="New Email" />
                <TextInput
                  specialType="email"
                  id="newEmailInput"
                  placeholder="New Email..."
                  value={props.data.newEmailInput}
                  onChange={props.functions.handleOnChange}
                  required
                  validateEmail
                  validateLength
                  minLength={0}
                  maxLength={100}
                />
              </div>
              {props.data.isEmailSend && (
                <div className="col-12 border" style={{ margin: "20px" }}>
                  <h5 className="property-text">
                    Confirmation email was sent to your email address!
                  </h5>
                </div>
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
            {props.data.isLoading ? (
              <button type="button" className="btn btn-success" disabled>
                <FontAwesomeIcon icon={faSync} spin />
              </button>
            ) : (
              <button
                type="button"
                className="btn btn-success"
                onClick={props.functions.handleOnSubmit}
              >
                Submit
              </button>
            )}
          </div>
        </div>
      </div>
    </div>
  );
}

export default ChangeEmailModalComponent;

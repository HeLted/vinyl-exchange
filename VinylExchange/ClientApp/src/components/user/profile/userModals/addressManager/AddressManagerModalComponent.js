import React from "react";

import AddAddressContainer from "./addAddress/AddAddressContainer";
import AddressesTableContainer from "./addressesTable/AddressesTableContainer";

function AddressManagerModalComponent(props) {
  return (
    <div className="modal" id="addressManagerModal" tabIndex="-1" role="dialog">
      <div className="modal-dialog" role="document">
        <div className="modal-content text-center">
          <div className="modal-header">
            <h5 className="property-text-nm modal-title">Address Manager</h5>
            <button
              type="button"
              className="btn btn-primary"
              onClick={props.functions.handleOnToggleModalPage}
            >
              {props.data.modalPageToggler ? "Add Address" : "Manage Addresses"}
            </button>
          </div>
          <div className="address-modal-body modal-body">
            <div className="row justify-content-center">
              {props.data.modalPageToggler ? (
                <AddressesTableContainer />
              ) : (
                <AddAddressContainer
                  functions={{
                    handleOnToggleModalPage:
                      props.functions.handleOnToggleModalPage
                  }}
                />
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

export default AddressManagerModalComponent;

import React, { Fragment } from "react";
import SingleSelect from "./../../../../../common/inputComponents/SingleSelect";
import Label from "./../../../../../common/inputComponents/Label";
import { Link } from "react-router-dom";

function PlaceOrderModalComponent(props) {
  const component = !props.data.isFlushModal && (
    <div className="modal" id="placeOrderModal" tabIndex="-1" role="dialog">
      <div className="modal-dialog" role="document">
        <div className="modal-content text-center">
          <div className="modal-header">
            <h5 className="property-text-nm modal-title">Place Order</h5>
          </div>
          <div className="address-modal-body modal-body">
            <div className="row justify-content-center">
              {props.data.userAddresses.length > 0 ? (
                <Fragment>
                  <Label
                    for="addressSelectInput"
                    value="Specify Delivery Address"
                  />
                  <SingleSelect
                    id="addressSelectInput"
                    value={props.data.addressSelectInput}
                    onChange={props.functions.handleOnChange}
                    options={props.data.userAddresses}
                    defaultOptionLabel="--Please Select Delivery Address--"
                  />
                </Fragment>
              ) : (
                <div className="no-addresses-container border">
                  <h6 className="property-text">
                    You don't have any registered addresses.
                  </h6>
                  
                  <Link
                    onClick={props.functions.handleFlushModal}
                    className="btn btn-primary"
                    to="/User/Profile"
                  >
                    
                    Go To Profile
                  
                  </Link>
                  <br/>
                 
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
            <button
              type="button"
              className="btn btn-success"
              onClick={props.functions.handleOnSubmit}
            >
              Place Order
            </button>
          </div>
        </div>
      </div>
    </div>
  );

  return component;
}

export default PlaceOrderModalComponent;

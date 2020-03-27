import React from "react";
import { PayPalButton } from "react-paypal-button-v2";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSync } from "@fortawesome/free-solid-svg-icons";

function PayNowModalComponent(props) {
  return (
    <div className="modal" id="payNowModal" tabIndex="-1" role="dialog">
      <div className="modal-dialog" role="document">
        <div className="modal-content text-center">
          <div className="modal-header">
            <h5 className="property-text-nm modal-title">
              Pay Now{" "}
              {props.data.isLoading && <FontAwesomeIcon icon={faSync} spin />}
            </h5>
          </div>
          <div className="address-modal-body modal-body">
            <PayPalButton
              amount={props.data.price + props.data.shippingPrice}
              // shippingPreference="NO_SHIPPING" // default is "GET_FROM_FILE"
              onSuccess={(details, data) => {
                props.functions.handleApprovePayment(data.orderID);
              }}
            />
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

export default PayNowModalComponent;

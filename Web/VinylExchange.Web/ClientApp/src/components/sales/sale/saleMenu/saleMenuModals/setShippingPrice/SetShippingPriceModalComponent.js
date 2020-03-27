import React from "react";
import Label from "./../../../../../common/inputComponents/Label";
import NumberInput from "./../../../../../common/inputComponents/NumberInput";

function SetShippingPriceModalComponent(props) {
  return (
    <div
      className="modal"
      id="setShippingPriceModal"
      tabIndex="-1"
      role="dialog"
    >
      <div className="modal-dialog" role="document">
        <div className="modal-content text-center">
          <div className="modal-header">
            <h5 className="property-text-nm modal-title">Set Shipping Price</h5>
          </div>
          <div className="address-modal-body modal-body">
            <div className="row justify-content-center">
              <div className="form-group">
                <Label for="priceInput" value="Price" />
                <div className="row">
                  <div className="col-2">
                    <div className="input-group-prepend">
                      <div className="input-group-text">$</div>
                    </div>
                  </div>
                  <div className="col-10">
                    <NumberInput
                      id="priceInput"
                      placeholder="Price"
                      value={props.data.priceInput}
                      onChange={props.functions.handleOnChange}
                      required
                      minNumber={0}
                      maxNumber={1000}
                      money
                    />
                  </div>
                </div>
              </div>
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
              onClick={props.functions.handleOnSubmit}
              type="button"
              className="btn btn-success"
            >
              Submit
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}

export default SetShippingPriceModalComponent;

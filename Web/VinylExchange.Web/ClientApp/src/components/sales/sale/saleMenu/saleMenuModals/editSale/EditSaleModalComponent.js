import React from 'react';
import Label from "./../../../../../common/inputComponents/Label";
import MoneyInput from "./../../../../../common/inputComponents/MoneyInput";
import TextAreaInput from "./../../../../../common/inputComponents/TextAreaInput";
import SingleSelect from "./../../../../../common/inputComponents/SingleSelect";
import {Link} from "react-router-dom";

function EditSaleModalComponent (props){
  return(<div className="modal fade" tabIndex="-1" id="editSaleModal" role="dialog">
  <div className="modal-dialog modal-dialog-centered" role="document">
    <div className="modal-content">
      <div className="modal-header">
        <h5 className="modal-title">Edit Sale</h5>
        <button type="submit" className="close" data-dismiss="modal">
          <span>&times;</span>
        </button>
      </div>
      <div className="modal-body">
        <form id="createSaleForm" onSubmit={props.functions.handleOnSubmit}>
          <div className="form-group">
            <Label for="vinylGradeInput" value="Vinyl Grade" />
            <SingleSelect
              id="vinylGradeInput"
              value={props.data.vinylGradeInput}
              onChange={props.functions.handleOnChange}
              options={props.data.gradeOptions}
              defaultOptionLabel="--Grade Vinyl--"
            />
          </div>

          <div className="form-group">
            <Label for="sleeveGradeInput" value="Sleeve Grade" />
            <SingleSelect
              id="sleeveGradeInput"
              value={props.data.sleeveGradeInput}
              onChange={props.functions.handleOnChange}
              options={props.data.gradeOptions}
              defaultOptionLabel="--Grade Sleeve--"
            />
          </div>
          <div className="form-group">
            <Label for="descriptionInput" value="Description" />
            <TextAreaInput
              id="descriptionInput"
              rows="3"
              value={props.data.descriptionInput}
              onChange={props.functions.handleOnChange}
            />
          </div>
          <div className="form-group">
            <Label for="priceInput" value="Price" />
            <div className="input-group mb-2">
              <div className="input-group-prepend">
                <div className="input-group-text">€</div>
              </div>
              <MoneyInput
                id="priceInput"
                placeholder="Price"
                value={props.data.priceInput}
                onChange={props.functions.handleOnChange}
              />
            </div>
            <div className="form-group">
              <Label for="shipsFromAddressSelectInput" value="Ships From" />
              {props.data.userAddresses.length > 0 ? (
                <SingleSelect
                  id="shipsFromAddressSelectInput"
                  value={props.data.shipsFromAddressSelectInput}
                  onChange={props.functions.handleOnChange}
                  options={props.data.userAddresses}
                  defaultOptionLabel="--Please Select Seller Address--"
                />
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
                  <br />
                </div>
              )}
            </div>
          </div>
        </form>
      </div>
      <div className="modal-footer">
        <button
          type="button"
          className="btn btn-secondary"
          data-dismiss="modal"
        >
          Close
        </button>
        <input
          type="submit"
          className="btn btn-outline-primary"
          form="createSaleForm"
          value="Edit Sale"
        />
      </div>
    </div>
  </div>
</div>);
}

export default EditSaleModalComponent;
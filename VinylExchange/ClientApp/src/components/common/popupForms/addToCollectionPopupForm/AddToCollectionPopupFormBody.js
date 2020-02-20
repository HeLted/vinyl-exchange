import React from "react";
import SingleSelect from "./../../inputComponents/SingleSelect";
import Label from "./../../inputComponents/Label";
import TextAreaInput from "./../../inputComponents/TextAreaInput";

function AddToCollectionPopupFormBody(props) {
  
  return (
    <div
      className="modal fade"
      id="exampleModalCenter"
      tabIndex="-1"
      role="dialog"
    >
      <div className="modal-dialog modal-dialog-centered" role="document">
        <div className="modal-content">
          <div className="modal-header">
            <h5 className="modal-title" id="exampleModalLongTitle">
              Add To Collection
            </h5>
            <button type="submit" className="close" data-dismiss="modal">
              <span>&times;</span>
            </button>
          </div>
          <div className="modal-body">
            <form
              id="addToCollectionForm"
              onSubmit={props.functions.handleOnSubmit}
            >
              <div className="form-group">
                <Label for="vinylGradeInput" value="Vinyl Grade" />
                <SingleSelect
                  id="vinylGradeInput"
                  value={props.data.vinylGrageInput}
                  onChange={props.functions.handleOnChange}
                  options={props.data.gradeOptions}
                  defaultOptionLabel="--Grade Vinyl--"
                />
              </div>

              <div className="form-group">
                <Label for="sleeveGradeInput" value="Sleeve Grade" />
                <SingleSelect
                  id="sleeveGradeInput"
                  value={props.data.sleeveGrageInput}
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
                  onChange={props.functions.handleOnChange}
                />
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
              form="addToCollectionForm"
              value="Add to Collection"
            />
          </div>
        </div>
      </div>
    </div>
  );
}

export default AddToCollectionPopupFormBody;

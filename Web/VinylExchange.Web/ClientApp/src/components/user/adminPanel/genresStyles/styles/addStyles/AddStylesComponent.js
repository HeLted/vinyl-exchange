import React, { Fragment } from "react";
import TextInput from "../../../../../common/inputComponents/TextInput";
import Label from "../../../../../common/inputComponents/Label";
import SingleSelect from "../../../../../common/inputComponents/SingleSelect";

function AddStylesComponent(props) {
  return (
    <Fragment>
      <div className="form-group">
        <Label for="genreSelectInput" value="Select Genre" />
        <SingleSelect
          id="genreSelectInput"
          value={props.data.genreSelectInput}
          onChange={props.functions.handleOnChange}
          options={props.data.genres}
          defaultOptionLabel="--Please Select Genre--"
        />
      </div>

      <div className="form-group">
        <Label for="styleNameInput" value="Style" />
        <TextInput
          id="styleNameInput"
          placeholder="Style Name..."
          value={props.data.styleNameInput}
         onChange={props.functions.handleOnChange}
         required
         validateLength
         minLength={3}
         maxLength={50}
        />
      </div>

      <br />
      <button
        class="btn btn-success btn-lg w-100"
        onClick={props.functions.handleOnSubmit}
      >
        Add Style
      </button>
    </Fragment>
  );
}

export default AddStylesComponent;

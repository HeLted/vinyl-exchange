import React from "react";
import TextInput from "../../../../../common/inputComponents/TextInput";
import Label from "../../../../../common/inputComponents/Label";

function AddGenresComponent(props) {
  return (
    <div className="form-group">
      <Label for="genreNameInput" value="Genre Name" />
      <TextInput
        id="genreNameInput"
        placeholder="Genre Name..."
        value={props.data.genreNameInput}
        onChange={props.functions.handleOnChange}
      />
      <br />
      <button
        class="btn btn-success btn-lg w-100"
        onClick={props.functions.handleOnSubmit}
      >
        Add Genre
      </button>
    </div>
  );
}

export default AddGenresComponent;

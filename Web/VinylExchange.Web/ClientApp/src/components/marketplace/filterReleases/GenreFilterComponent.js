import React from "react";
import SingleSelect from "./../../common/inputComponents/SingleSelect";
import Label from "./../../common/inputComponents/Label";

function GenreFilterComponent(props) {
  return (
    <div className="form-group">
      <Label for="genreSelectInput" value="Genre" />
      <SingleSelect
        id="genreSelectInput"
        value={props.data.genreSelectInput}
        onChange={props.functions.handleOnChange}
        options={props.data.genres}
      />
    </div>
  );
}

export default GenreFilterComponent;

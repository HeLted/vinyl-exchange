import React from "react";
import Select, { components } from "react-select";
import Label from "./../../common/inputComponents/Label";

function StyleFilterComponent(props) {
  return (
    <div className="form-group">
      <Label for="styleMultiSelectInput" value="Styles" />
      <Select
        id="styleMultiSelectInput"
        maxMenuHeight="100px"
        closeMenuOnSelect={false}
        isMulti
        onChange={props.functions.handleOnChangeMultiSelect}
        value={props.data.styleMultiSelectInput}
        options={props.data.styles}
        isDisabled={props.data.genreSelectInput === "" ? true : false}
      />
    </div>
  );
}

export default StyleFilterComponent;

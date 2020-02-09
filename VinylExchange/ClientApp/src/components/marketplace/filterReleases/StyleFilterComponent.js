import React from "react";
import Select, { components } from "react-select";
import Label from "./../../common/inputComponents/Label";

export default class StyleFilterComponent extends React.Component {
  render() {
    return (
      <div className="form-group">
        <Label for="styleMultiSelectInput" value="Styles" />
        <Select
          id="styleMultiSelectInput"
          closeMenuOnSelect={false}
          isMulti
        //   onChange={this.props.onChangeMultiSelect}
        //   value={this.props.state.styleMultiSelectInput}
        //   options={this.props.state.styles}
        />
      </div>
    );
  }
}

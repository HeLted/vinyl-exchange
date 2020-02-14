import React,{Component} from "react";
import Select, { components } from "react-select";
import Label from "./../../common/inputComponents/Label";

class StyleFilterComponent extends Component {
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

export default StyleFilterComponent
import React , {Component} from "react";
import SingleSelect from "./../../common/inputComponents/SingleSelect";
import Label from "./../../common/inputComponents/Label";

 class GenreFilterComponent extends Component {
  render() {
    return (
      <div className="form-group">
        <Label for="genreSelectInput" value="Genre" />
        <SingleSelect
          id="genreSelectInput"
            value={""}
            onChange={()=>{}}
            options={[]}
          defaultOptionLabel="--Please Select Genre--"
        />
      </div>
    );
  }
}

export default GenreFilterComponent;
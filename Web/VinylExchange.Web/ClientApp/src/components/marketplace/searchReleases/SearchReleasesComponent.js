import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSearch, faSync } from "@fortawesome/free-solid-svg-icons";
import TextInput from "./../../common/inputComponents/TextInput";

function SearchReleaseComponent(props) {
  const icon =
    props.isTyping === true ? (
      <FontAwesomeIcon icon={faSync} spin />
    ) : (
      <FontAwesomeIcon icon={faSearch} />
    );

  return (
    <div className="form-group">
      <div className="row m-0">
        <div className="col-lg-3 col-md-12 col-sm-12 col-xs-12 text-center p-0">
          <div className="input-group-prepend">
            <button className="btn btn-outline-primary w-100 " type="button" disabled>
              {icon}
            </button>
          </div>
        </div>
        <div className="col-lg-9 col-md-12 col-sm-12 col-xs-12 p-0">
          <TextInput
            id="searchBarInput"
            placeholder="Search..."
            value={props.searchInputValue}
            onChange={props.inputOnChangeFunction}
          />
        </div>
      </div>
    </div>
  );
}

export default SearchReleaseComponent;

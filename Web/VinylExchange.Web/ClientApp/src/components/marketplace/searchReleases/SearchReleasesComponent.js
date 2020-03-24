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
    <div className="input-group" id="searchBar">
      <span className="input-group-btn">
        <button className="btn btn-outline-primary" type="button" disabled>
          {icon}
        </button>
      </span>
      <TextInput
        id="searchBarInput"
        placeholder="Search..."
        value={props.searchInputValue}
        onChange={props.inputOnChangeFunction}
        
      />
    </div>
  );
}

export default SearchReleaseComponent;

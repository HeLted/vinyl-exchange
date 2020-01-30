import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSearch, faSync } from "@fortawesome/free-solid-svg-icons";

export default function SearchReleaseComponent(props) {
  const icon =
    props.isTyping === true ? (
      <FontAwesomeIcon icon={faSync} spin />
    ) : (
      <FontAwesomeIcon icon={faSearch} />
    );

  return (
    <div className="input-group" id="searchBar">
      <span className="input-group-btn">
        <button className="btn btn-outline-primary" type="button">
          {icon}
        </button>
      </span>
      <input
        id="searchBarInput"
        type="text"
        value={props.searchInputValue}
        onChange={props.inputOnChangeFunction}
        className="form-control"
        placeholder="Search..."
        aria-label="Search"
        aria-describedby="button-addon2"
      />
     
    </div>
  );
}

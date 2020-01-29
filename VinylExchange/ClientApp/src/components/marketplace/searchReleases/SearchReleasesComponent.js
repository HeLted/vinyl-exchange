import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSearch } from "@fortawesome/free-solid-svg-icons";

export default function SearchReleaseComponent() {
  return (
    <div className="input-group" id="searchBar">
      <input
        id="searchBarInput"
        type="text"
        className="form-control"
        placeholder="Search..."
        aria-label="Search"
        aria-describedby="button-addon2"
      />
      <span className="input-group-btn">
        <button className="btn btn-outline-primary" type="button">
          <FontAwesomeIcon icon={faSearch} />
        </button>
      </span>
    </div>
  );
}

import React from "react";
import TextInput from "../../../../../common/inputComponents/TextInput";
import Label from "../../../../../common/inputComponents/Label";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSync } from "@fortawesome/free-solid-svg-icons";

function AddAddressComponent(props) {
  return (
    <form onSubmit={props.functions.handleOnSubmit}>
      <div className="form-group">
        <Label for="countryInput" value="Country" />
        <TextInput
          id="countryInput"
          placeholder="Country..."
          value={props.data.countryInput}
          onChange={props.functions.handleOnChange}
        />
      </div>

      <div className="form-group">
        <Label for="townInput" value="Town" />
        <TextInput
          id="townInput"
          placeholder="Town..."
          value={props.data.townInput}
          onChange={props.functions.handleOnChange}
        />
      </div>

      <div className="form-group">
        <Label for="postalCodeInput" value="Postal Code" />
        <TextInput
          id="postalCodeInput"
          placeholder="Postal Code..."
          value={props.data.postalCodeInput}
          onChange={props.functions.handleOnChange}
        />
      </div>

      <div className="form-group">
        <Label for="fullAddressInput" value="Full Address" />
        <TextInput
          id="fullAddressInput"
          placeholder="Full Address..."
          value={props.data.fullAddressInput}
          onChange={props.functions.handleOnChange}
        />
      </div>
      <br />

      {props.data.isLoading ? (
        <button className="btn btn-success btn-lg">
          <FontAwesomeIcon icon={faSync} spin />
        </button>
      ) : (
        <button className="btn btn-success btn-lg">Add Address</button>
      )}
    </form>
  );
}

export default AddAddressComponent;

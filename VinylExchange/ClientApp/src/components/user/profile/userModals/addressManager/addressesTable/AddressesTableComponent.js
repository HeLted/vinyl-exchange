import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTimes, faCheck } from "@fortawesome/free-solid-svg-icons";
import BorderSpinner from "./../../../../../common/spinners/BorderSpinner";
import TextInput from "./../../../../../common/inputComponents/TextInput";

function AddressesTableComponent(props) {
  const addressRows = props.data.addresses.map(addressObj => {
    return (
      <tr key={addressObj.id} className="border property-text text-center">
        <td className="address-td">{addressObj.country}</td>
        <td className="address-td">{addressObj.town}</td>
        <td className="address-td">{addressObj.postalCode}</td>
        <td className="address-td">{addressObj.fullAddress}</td>
        <td className="address-td">
          <button
            class="btn btn-danger"
            onClick={() => props.functions.handleDeleteAddress(addressObj.id)}
          >
            <FontAwesomeIcon icon={faTimes} />
          </button>
        </td>
        
      </tr>
    );
  });

  return (
    <table className="table-hover">
      <tbody>
        {props.data.isLoading ? (
          <tr>
            <td>
              <BorderSpinner />
            </td>
          </tr>
        ) : addressRows.length === 0 ? (
          <tr className="border">
            <td className="address-td">
              <h6>You currentlly don't have registered addresses</h6>
            </td>
          </tr>
        ) : (
          addressRows
        )}
      </tbody>
    </table>
  );
}

export default AddressesTableComponent;

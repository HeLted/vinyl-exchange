import React, { Fragment } from "react";
import Label from "../../../../../common/inputComponents/Label";
import SingleSelect from "../../../../../common/inputComponents/SingleSelect";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTimes } from "@fortawesome/free-solid-svg-icons";

function RemoveStylesComponent(props) {
  const rows = props.data.styles.map(styleObj => {
    return (
      <tr className="border" key={styleObj.id}>
        <td className="property-text">{styleObj.name}</td>
        <td>
          <button
            className="btn btn-danger"
            onClick={() => props.functions.handleOnDeleteStyle(styleObj.id)}
          >
            <FontAwesomeIcon icon={faTimes} />
          </button>
        </td>
      </tr>
    );
  });

  return (
    <Fragment>
      <div className="form-group">
        <Label for="genreSelectInput" value="Select Genre" />
        <SingleSelect
          id="genreSelectInput"
          value={props.data.genreSelectInput}
          onChange={props.functions.handleOnChangeGenreSelect}
          options={props.data.genres}
          defaultOptionLabel="--Please Select Genre--"
        />
      </div>
      <div class="admin-remove-table  table-responsive table-responsive-sm">
        <table className="table">
          <tbody className="p-30 ">{rows}</tbody>
        </table>
      </div>
    </Fragment>
  );
}

export default RemoveStylesComponent;

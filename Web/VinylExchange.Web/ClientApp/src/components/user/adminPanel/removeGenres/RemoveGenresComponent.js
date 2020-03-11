import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTimes } from "@fortawesome/free-solid-svg-icons";

function RemoveGenresComponent(props) {
  const rows = props.data.genres.map(genreObj => {
    return (
      <tr className="border" key={genreObj.id}>
        <td>{genreObj.name}</td>
        <td>
          <button
            className="btn btn-danger"
            onClick={() => props.functions.handleOnDeleteGenre(genreObj.id)}
          >
            <FontAwesomeIcon icon={faTimes} />
          </button>
        </td>
      </tr>
    );
  });

  return (
    <table>
      <tbody className="p-30">{rows}</tbody>
    </table>
  );
}

export default RemoveGenresComponent;

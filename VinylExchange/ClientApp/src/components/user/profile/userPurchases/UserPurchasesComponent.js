import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faArrowRight} from "@fortawesome/free-solid-svg-icons";

function UserPurchsesComponent() {
  return (
    <table className="table-hover text-center" style={{ width: "100%" }}>
      <thead className="border">
        <tr>
          <th>Cover Art</th>
          <th>Artist</th>
          <th>Title</th>
          <th>Vinyl Grade</th>
          <th>Sleeve Grade</th>
          <th>Status</th>
        </tr>
      </thead>
      <tbody className="user-tbody ">
        <tr>
          <td>
            <img
              className="img-thumbnail"
              src="https://merchbar.imgix.net/product/105/6519/4041421258834/c3xXfSAh98-1.png?w=1280&h=1280&quality=60&auto=compress%252Cformat"
              height="50px"
              width="50px"
            />
          </td>
          <td>Aphex Twin</td>
          <td>Classics</td>
          <td>Good</td>
          <td>Fair</td>
          <td>Open</td>
          <td><button className="btn btn-primary"><FontAwesomeIcon icon={faArrowRight}/></button></td>
        </tr>
      </tbody>
    </table>
  );
}

export default UserPurchsesComponent;

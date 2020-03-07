import React from "react";
import { Url } from "./../../../../constants/UrlConstants";
import BorderSpinner from "./../../../common/spinners/BorderSpinner";
import GoToSaleButton from "./../../../common/GoToSaleButton";
import GradeBadge from "./../../../common/badges/GradeBadge";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faBars } from "@fortawesome/free-solid-svg-icons";
import StatusBadge from "./../../../common/badges/StatusBadge";

function UserSalesComponent(props) {
  const rows = props.data.sales.map(saleObj => {
    return (
      <tr onClick={() => props.functions.handleGoToSale(saleObj.id)} key={saleObj.id}>
        <td>
          <img
            className="img-thumbnail"
            src={
              Url.mediaStorage +
              saleObj.coverArt.path +
              saleObj.coverArt.fileName
            }
            height="50px"
            width="50px"
          />
        </td>
        <td>{saleObj.artist}</td>
        <td>{saleObj.title}</td>
        <td>
          <GradeBadge data={{ grade: saleObj.vinylGrade }} />
        </td>
        <td>
          <GradeBadge data={{ grade: saleObj.sleeveGrade }} />
        </td>
        <td><StatusBadge data={{status:saleObj.status}}/></td>
      </tr>
    );
  });

  return (
    <div class="table-responsive-md table-responsive-sm">
    <table className="table-hover text-center" style={{ width: "100%" }}>
      <thead className="border property-text-nm">
        <tr>
          <th className="border-left border-right">Cover Art</th>
          <th className="border-left border-right">Artist</th>
          <th className="border-left border-right">Title</th>
          <th className="border-left border-right">Vinyl Grade</th>
          <th className="border-left border-right">Sleeve Grade</th>
          <th className="border-left border-right">Status</th>
        </tr>
      </thead>
      <tbody className="normal-tbody">
        {props.data.isLoading ? (
          <tr>
            <td colSpan="6">
              <BorderSpinner />
            </td>
          </tr>
        ) : rows.length > 0 ? (
          rows
        ) : (
          <tr className="border">
            <td colSpan="6" className="no-addresses-container">
              <h6>You currentlly don't have any sales</h6>
            </td>
          </tr>
        )}
      </tbody>
    </table>
    </div>
  );
}

export default UserSalesComponent;

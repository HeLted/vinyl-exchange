import React from "react";
import { Url } from "./../../../../constants/UrlConstants";
import BorderSpinner from "./../../../common/spinners/BorderSpinner";
import GradeBadge from "./../../../common/badges/GradeBadge";
import StatusBadge from "../../../common/badges/StatusBadge";


function UserPurchsesComponent(props) {
  const rows = props.data.purchases.map(purchaseObj => {
    return (
      <tr onClick={() => props.functions.handleGoToSale(purchaseObj.id)} key={purchaseObj.id}>
        <td>
          <img
            className="img-thumbnail"
            src={
              Url.mediaStorage +
              purchaseObj.coverArt.path +
              purchaseObj.coverArt.fileName
            }
            height="50px"
            width="50px"
          />
        </td>
        <td>{purchaseObj.artist}</td>
        <td>{purchaseObj.title}</td>
        <td><GradeBadge data={{grade:purchaseObj.vinylGrade}}/></td>
        <td><GradeBadge data={{grade:purchaseObj.sleeveGrade}}/></td>
        <td><StatusBadge data={{status:purchaseObj.status}}/></td>
        
      </tr>
    );
  });

  return (<div class="table-responsive-md table-responsive-sm">
    <table className="table-hover text-center " style={{ width: "100%" }}>
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
              <h6>You currentlly don't have any purchases</h6>
            </td>
          </tr>
        )}
      </tbody>
    </table>
    </div>
  );
}

export default UserPurchsesComponent;

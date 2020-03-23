import React from "react";
import GoToSaleButton from "./../../../common/GoToSaleButton";
import BorderSpinner from "./../../../common/spinners/BorderSpinner";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faBars,
  faUserCircle,
  faEuroSign
} from "@fortawesome/free-solid-svg-icons";
import GradeBadge from "./../../../common/badges/GradeBadge";
import UserThumbnail from "./../../../common/UserThumbnail";
import PriceTagBadge from "./../../../common/badges/PriceTagBadge"

function ReleaseSalesComponent(props) {
  const salesRows = props.data.sales.map(saleObj => {
    return (
      <tr key={saleObj.id}>
        <td>
          {" "}
          <UserThumbnail data={{ userId: saleObj.sellerId }} />
        </td>
        <td className="property-text-nm">{saleObj.sellerUsername}</td>
        <td>
          <GradeBadge data={{ grade: saleObj.vinylGrade }} />
        </td>
        <td>
          <GradeBadge data={{ grade: saleObj.sleeveGrade }} />
        </td>
        <td className="property-text-nm">
          <PriceTagBadge data={{ price: saleObj.price }} />{" "}
        
        </td>
        <td>
          <GoToSaleButton data={{ saleId: saleObj.id }} />
        </td>
      </tr>
    );
  });

  const component = props.data.isLoading ? (
    <tr>
      <td colSpan="6">
        <BorderSpinner />
      </td>
    </tr>
  ) : salesRows.length > 0 ? (
    salesRows
  ) : (
    <tr className="border">
      <td colSpan="6" className="no-addresses-container">
        <h6>There are currently no active sales for this release</h6>
      </td>
    </tr>
  );

  return (
    <div className="release-sales-table  table-responsive table-responsive-sm">
      <table className="table table-bordered table-hover text-center">
        <thead>
          <tr>
            <th className="border-left border-right">
              {" "}
              <FontAwesomeIcon icon={faUserCircle} />
            </th>
            <th className="border-left border-right">Seller</th>
            <th className="border-left border-right">Vinyl Grade</th>
            <th className="border-left border-right">Sleeve Grade</th>
            <th className="border-left border-right">Price</th>
            <th className="border-left border-right">
              <FontAwesomeIcon icon={faBars} />
            </th>
          </tr>
        </thead>
        <tbody className="normal-tbody">{component}</tbody>
      </table>
    </div>
  );
}

export default ReleaseSalesComponent;

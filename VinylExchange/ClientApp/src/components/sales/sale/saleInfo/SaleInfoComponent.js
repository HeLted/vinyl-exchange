import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faUser,
  faCompactDisc,
  faSquareFull
} from "@fortawesome/free-solid-svg-icons";
import UserThubnail from "./../../../common/UserThumbnail"

function SaleInfoComponent(props) {
  const sale = props.data.sale;

  return (
    <div>
      <div className="row ">
      <h5 className="property-text">
        <FontAwesomeIcon icon={faUser} /> Seller : 
         <UserThubnail  data={{ userId: sale.sellerId }} /> {sale.sellerUsername}
      </h5>
      </div>
       <div className="row ">
      {(sale.buyerId !== "" && sale.buyerId !== null) ? (
        <h5 className="property-text">
          <FontAwesomeIcon icon={faUser} /> Buyer : 
           <UserThubnail key={sale.buyerId} data={{ userId: sale.buyerId }} /> {sale.buyerUsername}
        </h5>
      ): null}
      
      </div>
       <div className="row ">
      <h5 className="property-text">
        <FontAwesomeIcon icon={faCompactDisc} /> Vinyl Condition :{" "}
        {sale.vinylConditionText}
      </h5>
       </div>
       <div className="row ">
      <h5 className="property-text">
        <FontAwesomeIcon icon={faSquareFull} /> Sleeve Condition :{" "}
        {sale.sleeveConditionText}
      </h5>
      </div>
    </div>
  );
}

export default SaleInfoComponent;

import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faUser ,faCompactDisc, faSquareFull} from "@fortawesome/free-solid-svg-icons";

function SaleInfoComponent(props) {
  const sale = props.data.sale;

  return (
    <div>
      <h5 className="property-text"><FontAwesomeIcon icon={faUser}/> Seller : {sale.sellerUsername}</h5>
      <h5 className="property-text"><FontAwesomeIcon icon={faCompactDisc}/> Vinyl Condition : {sale.vinylConditionText}</h5>
      <h5 className="property-text"><FontAwesomeIcon icon={faSquareFull}/> Sleeve Condition : {sale.sleeveConditionText}</h5>
    </div>
  );
}

export default SaleInfoComponent;

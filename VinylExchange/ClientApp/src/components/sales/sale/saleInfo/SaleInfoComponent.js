import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faUser,
  faCompactDisc,
  faSquareFull
} from "@fortawesome/free-solid-svg-icons";
import UserThubnail from "./../../../common/UserThumbnail";
import GradeBadge from "./../../../common/badges/GradeBadge";
import PriceTagBadge from "./../../../common/badges/PriceTagBadge";

function SaleInfoComponent(props) {
  const sale = props.data.sale;

  return (
    <div>
      <div className="row text-center">
        <div
          className={
            sale.buyerId === "" || sale.buyerId === null ? "col-12" : "col-6"
          }
        >
          <div className="border border-dark">
            <h5 className="property-text-lm">
              <b>Seller</b>
            </h5>
          </div>
          <br />

          <div className="row justify-content-center">
            <UserThubnail data={{ userId: sale.sellerId }} />
          </div>
          <div className="row  justify-content-center">
            <h5 className="property-text-nm">{sale.sellerUsername}</h5>
          </div>
          <br />
        </div>

        {sale.buyerId !== "" && sale.buyerId !== null ? (
          <div className="col-6">
            <div className="border border-dark">
              <h5 className="property-text-lm">
                <b>Buyer</b>
              </h5>
            </div>
            <br />

            <div className="row justify-content-center">
              <UserThubnail data={{ userId: sale.buyerId }} />
            </div>
            <div className="row  justify-content-center">
              <h5 className="property-text-nm">{sale.buyerUsername}</h5>
            </div>
            <br />
          </div>
        ) : null}

        <div className="col-6">
          <div className="border border-dark">
            <h5 className="property-text-lm">
              <b>Vinyl Grade</b>
            </h5>
          </div>
          <h5 className="property-text">
            <GradeBadge data={{ grade: sale.vinylGrade }} />
          </h5>
        </div>
        <div className="col-6">
          <div className="border border-dark">
            <h5 className="property-text-lm">
              <b> Sleeve Grade</b>
            </h5>
          </div>
          <h5 className="property-text">
            <GradeBadge data={{ grade: sale.sleeveGrade }} />
          </h5>
        </div>
        <div className="col-6">
          <div className="border border-dark">
            <h5 className="property-text-lm">
              <b> Price</b>
            </h5>
          </div>
          <h5 className="property-text">
            <PriceTagBadge data={{ price: sale.price }} />
          </h5>
        </div>
        <div className="col-6">
          <div className="border border-dark">
            <h5 className="property-text-lm">
              <b>Shipping Price</b>
            </h5>
          </div>
          <h5 className="property-text">
            <PriceTagBadge data={{ price: sale.shippingPrice }} />
          </h5>
        </div>
        <div className="col-12">
          <div className="border border-dark">
            <h5 className="property-text-lm">
              <b>Description</b>
            </h5>
          </div>
          <br/>
          <div className="border border-dark">
          <h5 className="property-text">
          {sale.description}
          </h5>
          </div>
        </div>
      </div>
    </div>
  );
}

export default SaleInfoComponent;

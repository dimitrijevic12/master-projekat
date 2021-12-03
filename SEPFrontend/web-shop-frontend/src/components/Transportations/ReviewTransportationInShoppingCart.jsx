import React, { useEffect, useState } from "react";
import { getTransportationById } from "../../actions/actionsTransportations";
import { connect } from "react-redux";
import { compose } from "redux";
import "react-toastify/dist/ReactToastify.css";
import BuyTransportationModal from "./BuyTransportationModal";
import DateTimePicker from "react-datetime-picker";
import "react-datepicker/dist/react-datepicker.css";
import "../../css/datepicker.css";

function ReviewTransportation(props) {
  const [item, setItem] = useState({});
  const [showPostModal, setShowPostModal] = useState(false);

  useEffect(() => {
    debugger;
    props.getTransportationById(
      localStorage.getItem("shoppingTransportation-id")
    );
  }, []);

  const Transportation = () => {
    debugger;
    if (props.loadedImage === undefined) {
      return null;
    }
    return (
      <img
        src={"data:image/jpg;base64," + props.loadedImage.fileContents}
        style={{ width: 600, height: 320 }}
        className="mb-3"
      />
    );
  };

  const displayModalPost = (item) => {
    if (item != undefined) {
      setItem(item);
    }
    if (
      localStorage.getItem("shoppingCart") === null ||
      localStorage.getItem("shoppingCart") === ""
    ) {
      var shoppingCartList = [];
    } else {
      var shoppingCartList = JSON.parse(localStorage.getItem("shoppingCart")); //get them back
    }
    shoppingCartList = shoppingCartList.filter((cartItem) => {
      if (cartItem.type === "transportation") {
        return cartItem.item.id !== item.id;
      } else {
        return cartItem;
      }
    });
    localStorage.setItem("shoppingCart", JSON.stringify(shoppingCartList));
    window.location = "/items-in-shopping-cart";
  };

  if (props.transportation === undefined) {
    return null;
  }

  return (
    <React.Fragment>
      {showPostModal ? (
        <BuyTransportationModal
          show={showPostModal}
          item={item}
          onShowChange={() => displayModalPost()}
        />
      ) : null}
      <div className="mt-5">
        <div className="d-inline-flex w-50">
          <div class="form-group w-100 pr-5">
            <Transportation />
          </div>
        </div>
        <div className="d-inline-flex w-50">
          <div class="form-group w-100 pr-5">
            <label className="label">Name:</label>
            <input
              type="text"
              name="name"
              value={props.transportation.name}
              disabled={true}
              class="form-control"
              id="name"
              placeholder="Enter name of product"
            />
          </div>
        </div>
      </div>
      <div className="mt-5">
        <div className="d-inline-flex w-50">
          <div class="form-group w-100 pr-5">
            <label for="tag">Date:</label>
            <div className="d-block w-100">
              <DateTimePicker
                id="datepickerInput"
                name="start"
                disabled={true}
                value={props.transportation.departureTime}
                minDate={new Date()}
                selected={props.transportation.departureTime}
              />
            </div>
          </div>
        </div>
      </div>
      <div className="mt-5">
        <div className="d-inline-flex w-50">
          <div class="form-group w-100 pr-5">
            <label for="tag">Start Destination:</label>
            <input
              type="text"
              name="startDestination"
              value={props.transportation.startDestination}
              disabled={true}
              class="form-control"
              id="startDestination"
              placeholder="Enter start destination"
            />
          </div>
        </div>
        <div className="d-inline-flex w-50">
          <div class="form-group w-100 pr-5">
            <label for="location">Final Destination:</label>
            <input
              type="text"
              name="finalDestination"
              value={props.transportation.finalDestination}
              disabled={true}
              class="form-control"
              id="finalDestination"
              placeholder="Enter final destination"
            />
          </div>
        </div>
      </div>
      <div className="mt-5">
        <div className="d-inline-flex w-50">
          <div class="form-group w-100 pr-5">
            <label for="tag">Price:</label>
            <input
              type="number"
              name="price"
              value={props.transportation.price}
              disabled={true}
              class="form-control"
              id="price"
              placeholder="Enter price"
            />
          </div>
        </div>
        <div className="d-inline-flex w-50">
          <div class="form-group w-100 pr-5">
            <label for="location">Description:</label>
            <textarea
              name="description"
              value={props.transportation.description}
              cols="40"
              rows="5"
              disabled={true}
              class="form-control"
              placeholder="Enter description"
            ></textarea>
          </div>
        </div>
      </div>
      <div className="mt-5">
        <div className="d-inline-flex w-50">
          <div class="form-group w-100 pr-5">
            <label for="location">Total price:</label>
            <input
              type="text"
              name="quantity"
              class="form-control"
              id="quantity"
              value={
                parseInt(localStorage.getItem("quantity-for-shopping-item")) *
                  parseInt(props.transportation.price) +
                " €"
              }
              disabled={true}
              placeholder="Enter quantity"
            />
          </div>
        </div>
      </div>
      <div style={{ textAlign: "center" }} className="mt-5 pb-5">
        <button
          onClick={() => displayModalPost(props.transportation)}
          className="btn btn-primary"
        >
          Remove from shopping cart
        </button>
      </div>
    </React.Fragment>
  );
}

const mapStateToProps = (state) => ({
  transportation: state.transportation,
  loadedImage: state.loadedImage,
});

export default compose(
  connect(mapStateToProps, {
    getTransportationById,
  })
)(ReviewTransportation);

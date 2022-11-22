import React from 'react';
import { useState } from 'react';
import { useFormik } from 'formik';
import * as Yup from 'yup';
import axios from 'axios';
import CustomRoommates from './CustomRoommates'
import "yup-phone";
import { LithuanianCities } from './LithuanianCities.js';
import CitySelect from './CitySelect';
import Slider from 'react-input-slider';

const options = [
    { value: 1, label: '1' },
    { value: 2, label: '2' },
    { value: 3, label: '3+' },
]

const cityOptions = []

export const EditListingComponent = (props) => {

    for(let i = 0; i < LithuanianCities.length; ++i){
        cityOptions[i] = {value: i, label: LithuanianCities[i]}
    }

    const [responseMessage, setResponseMessage] = useState("");

    const [state, setState] = useState({ x: 0 });

    const formik = useFormik({
        initialValues: {
            id: props.listing.id,
            firstName: props.listing.firstName,
            lastName: props.listing.lastName,
            email: props.listing.email,
            phone: props.listing.phone,
            city: props.listing.city,
            maxPrice: props.listing.maxPrice,
            roommateCount: props.listing.roommateCount,
            extraComment: props.listing.extraComment,
            date: props.listing.date,
            userid: props.listing.userId,
            user: props.listing.user
        },
        validationSchema: Yup.object({
            phone: Yup
                .string()
                .phone("LT")
                .required("Required"),
            city: Yup
                .string()
                .max(30, "City can only be up to 30 characters")
                .required("Required"),
            maxPrice: Yup
                .number()
                .typeError("Only use digits")
                .max(999999, "The price exceeds the maximum limit")
                .required("Required"),
            extraComment: Yup
                .string()
                .max(200, "Extra Comment can only be up to 200 characters")
        }),
        onSubmit: (values) => {
            console.log(values);
            axios({
                method: 'post',
                url: 'https://localhost:44332/listing/update',
                data: values
            }).then((response) => {
                props.toggleEditListing(null, false);
            })
            }
        }
    )

    return (
        <>
        <div className="centered-container edit-listing-container">
            <form onSubmit={formik.handleSubmit}>
                <div className="form-field-container-flex">
                    <div className="form-field-flex"><label htmlFor="phone">Phone Number</label></div>
                    <div className="form-field-flex">
                        <input
                            name="phone"
                            id="phone"
                            type="phone"
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                            value={formik.values.phone}
                        />
                        {
                            formik.touched.phone && formik.errors.phone && 
                                <p style={{ color: "red", margin: "0", padding: "0", fontSize: "10px" }}>
                                    {formik.errors.phone}
                                </p>
                        }
                    </div>
                </div>
                <div className="form-field-container-flex">
                    <div className="form-field-flex"><label htmlFor="roomateCount">Number of Roommates</label></div>
                    <div className="form-field-flex">
                        <CustomRoommates
                            options={options}
                            value={formik.values.roommateCount}
                            className={"roommateCount"}
                            onChange={value => formik.setFieldValue('roommateCount',value.value)}
                        />
                    </div>
                </div>
                <div className="form-field-container-flex">
                    <div className="form-field-flex"><label htmlFor="price">Maximum Price:</label></div>
                        <div className="form-field-flex" style={{ height: "150px" }}>
                            <Slider
                                styles={{
                                    track: {
                                    },
                                    active: {
                                        backgroundColor:'black'
                                    },
                                    thumb: {
                                        backgroundColor: 'white',
                                        width: 20,
                                        height: 20
                                    },
                                    disabled: {
                                        opacity: 0.5
                                    }
                                }}
                                xmax={5000}
                                axis="x"
                                x={state.x}
                                xStep={50}
                                name="maxPrice"
                                id="maxPrice"
                                type="maxPrice"
                                onChange={({ x }) => setState(state => ({ ...state, x }))}
                                value={formik.values.maxPrice = state.x}>
                            </Slider>
                            <div>{state.x} &#8364;</div>
                    </div>
                </div>
                <div className="form-field-container-flex">
                    <div className="form-field-flex"><label htmlFor="extraComment">Extra Comment * </label></div>
                    <div className="form-field-flex">
                        <input
                            name="extraComment"
                            id="extraComment"
                            type="extraComment"
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                            value={formik.values.extraComment}
                        />
                        {
                            formik.touched.extraComment && formik.errors.extraComment &&
                                <p style={{ color: "red", margin: "0", padding: "0", fontSize: "10px" }}>
                                    {formik.errors.extraComment}
                                </p>
                        }
                    </div>
                </div>
                <button type="submit">Update</button>
            </form>
            <small>   * - Optional Fields</small>
            <p>{responseMessage}</p>
        </div>
        </>
    )
}
 

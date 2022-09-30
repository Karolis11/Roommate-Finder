import React, { useState } from 'react';
import { useFormik } from 'formik';
import * as Yup from 'yup';
import axios from 'axios';
import { useSnackbar } from 'notistack'

export const Signup = (props) => {
    const [redirectionMsg, setRedirectionMsg] = useState("");
    const { enqueueSnackbar, closeSnackbar } = useSnackbar()

    const formik = useFormik({
        initialValues: {
            firstName: "",
            lastName: "",
            email: "",
            password: "",
            city: ""
        },
        validationSchema: Yup.object({
            firstName: Yup
                        .string()
                        .max(20, "Name can only be up to 20 characters")
                        .required("Required"),
            lastName: Yup
                        .string()
                        .max(20, "Lastname can only be up to 20 characters")
                        .required("Required"),
            email: Yup
                        .string()
                        .email("Invalid email address")
                        .required("Required"),
            password: Yup
                        .string()
                        .min(6, "Password must be at least 6 characters long")
                        .max(20,"Password can only be up to 20 characters")
                        .required("Required"),
            city: Yup
                        .string()
                        .max(30, "City can only be up to 30 characters")
                        .required("Required")
        }),
        onSubmit: (values) => {
            axios({
                method: 'post',
                url: 'https://localhost:44332/registration',
                data: values
            }).then((response) => {

                // if account was created, redirect in 5 seconds
                if (response.data.IsSuccess) {

                    enqueueSnackbar(response.data.Message, {variant: "success"});

                    let i = 5;
                    setRedirectionMsg(`Redirecting in ${i}`);
                    let interval = setInterval(() => {
                        i--;
                        if (i < 0) {
                            props.toggleSignUp(false);
                            clearInterval(interval);
                        }
                        setRedirectionMsg(`Redirecting in ${i}`);
                    }, 1000);
                } else {
                    enqueueSnackbar(response.data.Message, {variant: "error"});
                }
            })  
        }
    })

    return (
        <>
        <form onSubmit={formik.handleSubmit}>
            <table>
                <tbody>
                    <tr>
                        <td><label htmlFor="firstName">First name</label></td>
                        <td>
                            <input 
                                name="firstName" 
                                id="firstName" 
                                type="text" 
                                onChange={formik.handleChange}
                                onBlur={formik.handleBlur}
                                value={formik.values.firstName}
                                />
                            { 
                                formik.touched.firstName && formik.errors.firstName &&
                                    <p style={{color: "red", margin:"0", padding: "0", fontSize: "10px"}}>
                                        {formik.errors.firstName}
                                    </p> 
                            }
                        </td>
                    </tr>
                    <tr>
                        <td><label htmlFor="lastName">Last name</label></td>
                        <td>
                            <input 
                                name="lastName" 
                                id="lastName" 
                                type="text"
                                onChange={formik.handleChange}
                                onBlur={formik.handleBlur}
                                value={formik.values.lastName}
                            />
                            { 
                                formik.touched.lastName && formik.errors.lastName &&
                                    <p style={{color: "red", margin:"0", padding: "0", fontSize: "10px"}}>
                                        {formik.errors.lastName}
                                    </p> 
                            }
                        </td>
                    </tr>
                    <tr>
                        <td><label htmlFor="email">Email</label></td>
                        <td>
                            <input 
                                name="email" 
                                id="email" 
                                type="email"
                                onChange={formik.handleChange}
                                onBlur={formik.handleBlur}
                                value={formik.values.email}
                            />
                            { 
                                formik.touched.email && formik.errors.email &&
                                    <p style={{color: "red", margin:"0", padding: "0", fontSize: "10px"}}>
                                        {formik.errors.email}
                                    </p> 
                            }
                        </td>
                    </tr>
                    <tr>
                        <td><label htmlFor="password">Password</label></td>
                        <td>
                            <input 
                                name="password" 
                                id="password" 
                                type="password"
                                onChange={formik.handleChange}
                                onBlur={formik.handleBlur}
                                value={formik.values.password}
                            />
                            { 
                                formik.touched.password && formik.errors.password &&
                                    <p style={{color: "red", margin:"0", padding: "0", fontSize: "10px"}}>
                                        {formik.errors.password}
                                    </p> 
                            }
                        </td>
                    </tr>
                    <tr>
                        <td><label htmlFor="city">City</label></td>
                        <td>
                            <input 
                                name="city" 
                                id="city" 
                                type="text"
                                onChange={formik.handleChange}
                                onBlur={formik.handleBlur}
                                value={formik.values.city}
                            />
                            { 
                                formik.touched.city && formik.errors.city &&
                                    <p style={{color: "red", margin:"0", padding: "0", fontSize: "10px"}}>
                                        {formik.errors.city}
                                    </p> 
                            }
                        </td>
                    </tr>
                </tbody>
            </table>
            <button type="submit">Submit</button>
        </form>
        <p>{redirectionMsg}</p>
        </>
    )
}
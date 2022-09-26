import React, { useState } from 'react';
import { useFormik } from 'formik';
import * as Yup from 'yup';
import axios from 'axios';

export const Login = (props) => {

    const [responseMessage, setResponseMessage] = useState("");

    const formik = useFormik({
        initialValues: {
            email: "",
            password: "",
            firstName: "",
            lastName: "",
            city: ""
        },
        validationSchema: Yup.object({
            email: Yup
                        .string()
                        .required("Required"),
            password: Yup
                        .string()
                        .required("Required"),
        }),
        onSubmit: (values) => {
            axios({
                method: 'post',
                url: 'https://localhost:44332/login',
                data: values
            })
            .then((response) => {
                console.log(response.data);
                if(response.data.Success){ 
                    props.toggleLoggedIn(true); 
                    props.toggleLogin(false);
                }
                else{
                    setResponseMessage(response.data.Message);
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
                                formik.touched.email && formik.errors.email 
                                ? 
                                    <p style={{color: "red", margin:"0", padding: "0", fontSize: "10px"}}>
                                        {formik.errors.email}
                                    </p> 
                                : null 
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
                                formik.touched.password && formik.errors.password 
                                ? 
                                    <p style={{color: "red", margin:"0", padding: "0", fontSize: "10px"}}>
                                        {formik.errors.email}
                                    </p> 
                                : null 
                            }
                        </td>
                    </tr>
                    
                </tbody>
            </table>
            <button type="submit">Submit</button>
        </form>
        <p>{responseMessage}</p>
        </>
    )
}
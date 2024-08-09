import React, { useContext, useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import styles from "../components/testpage/Testpage.module.css";
import PawnWhite from "../assets/pieces/pawn-white.png";

function TestPage() {

    return (
        <div className={styles.main_div}>
            <div className={styles.div1}>
            </div>
            <div className={styles.div2}>
                <div className={styles.div_tr}>
                    <div className={styles.div_td}>11</div>
                    <div className={styles.div_td}>12</div>
                    <div className={styles.div_td}>13</div>
                    <div className={styles.div_td}>14</div>
                    <div className={styles.div_td}>15</div>
                    <div className={styles.div_td}>16</div>
                    <div className={styles.div_td}>17</div>
                    <div className={styles.div_td}>18</div>
                </div>
                <div className={styles.div_tr}>
                    <div className={styles.div_td}>21</div>
                    <div className={styles.div_td}>22</div>
                    <div className={styles.div_td}>23</div>
                    <div className={styles.div_td}>24</div>
                    <div className={styles.div_td}>25</div>
                    <div className={styles.div_td}>26</div>
                    <div className={styles.div_td}>27</div>
                    <div className={styles.div_td}>28</div>
                </div>
                <div className={styles.div_tr}>
                    <div className={styles.div_td}>31</div>
                    <div className={styles.div_td}>
                        <div className={styles.div_optical}>
                            <img src={PawnWhite} />
                            <span className={styles.dot}/>
                        </div>
                    </div>
                    <div className={styles.div_td}>33</div>
                    <div className={styles.div_td}>
                        <img src={PawnWhite} />
                    </div>
                    <div className={styles.div_td}>35</div>
                    <div className={styles.div_td}>36</div>
                    <div className={styles.div_td}>37</div>
                    <div className={styles.div_td}>38</div>
                </div>
                <div className={styles.div_tr}>
                    <div className={styles.div_td}>41</div>
                    <div className={styles.div_td}>42</div>
                    <div className={styles.div_td}>43</div>
                    <div className={styles.div_td}>44</div>
                    <div className={styles.div_td}>45</div>
                    <div className={styles.div_td}>46</div>
                    <div className={styles.div_td}>47</div>
                    <div className={styles.div_td}>48</div>
                </div>
                <div className={styles.div_tr}>
                    <div className={styles.div_td}>51</div>
                    <div className={styles.div_td}>52</div>
                    <div className={styles.div_td}>53</div>
                    <div className={styles.div_td}><span className={styles.dot} /></div>
                    <div className={styles.div_td}>55</div>
                    <div className={styles.div_td}>56</div>
                    <div className={styles.div_td}>57</div>
                    <div className={styles.div_td}>58</div>
                </div>
                <div className={styles.div_tr}>
                    <div className={styles.div_td}>61</div>
                    <div className={styles.div_td}>62</div>
                    <div className={styles.div_td}>63</div>
                    <div className={styles.div_td}>64</div>
                    <div className={styles.div_td}>65</div>
                    <div className={styles.div_td}>66</div>
                    <div className={styles.div_td}>67</div>
                    <div className={styles.div_td}>68</div>
                </div>
                <div className={styles.div_tr}>
                    <div className={styles.div_td}>71</div>
                    <div className={styles.div_td}>72</div>
                    <div className={styles.div_td}>73</div>
                    <div className={styles.div_td}>74</div>
                    <div className={styles.div_td}>75</div>
                    <div className={styles.div_td}>76</div>
                    <div className={styles.div_td}>77</div>
                    <div className={styles.div_td}>78</div>
                </div>
                <div className={styles.div_tr}>
                    <div className={styles.div_td}>81</div>
                    <div className={styles.div_td}>82</div>
                    <div className={styles.div_td}>83</div>
                    <div className={styles.div_td}>84</div>
                    <div className={styles.div_td}>85</div>
                    <div className={styles.div_td}>86</div>
                    <div className={styles.div_td}>87</div>
                    <div className={styles.div_td}>88</div>
                </div>
            </div>
        </div>

    );

    /* Table
    return (
        <div className={styles.main_div}>
            <div className={styles.div1}>
            </div>
            <div className={styles.div2}>

                <div className={styles.div_table}>
                    <table>
                        <tbody>
                            <tr>
                                <td><div className={styles.div3}>div3</div></td>
                                <td>2</td>
                                <td>3</td>
                                <td>4</td>
                                <td>5</td>
                                <td>6</td>
                                <td>7</td>
                                <td>8</td>
                            </tr>
                            <tr>
                                <td>9</td>
                                <td>10</td>
                                <td>11</td>
                                <td>12</td>
                                <td>13</td>
                                <td>14</td>
                                <td>15</td>
                                <td>16</td>
                            </tr>
                            <tr>
                                <td>19</td>
                                <td>110</td>
                                <td>111</td>
                                <td><img src={PawnWhite}/></td>
                                <td>113</td>
                                <td>114</td>
                                <td>115</td>
                                <td>116</td>
                            </tr>
                            <tr>
                                <td>1</td>
                                <td>2</td>
                                <td>3</td>
                                <td>4</td>
                                <td>5</td>
                                <td>6</td>
                                <td>7</td>
                                <td>8</td>
                            </tr>
                            <tr>
                                <td>1</td>
                                <td>2</td>
                                <td>3</td>
                                <td>4</td>
                                <td>5</td>
                                <td>6</td>
                                <td>7</td>
                                <td>8</td>
                            </tr>
                            <tr>
                                <td>1</td>
                                <td>2</td>
                                <td>3</td>
                                <td>4</td>
                                <td>5</td>
                                <td>6</td>
                                <td>7</td>
                                <td>8</td>
                            </tr>
                            <tr>
                                <td>1</td>
                                <td>2</td>
                                <td>3</td>
                                <td>4</td>
                                <td>5</td>
                                <td>6</td>
                                <td>7</td>
                                <td>8</td>
                            </tr>
                            <tr>
                                <td>1</td>
                                <td>2</td>
                                <td>3</td>
                                <td>4</td>
                                <td>5</td>
                                <td>6</td>
                                <td>7</td>
                                <td>8</td>
                            </tr>
                        </tbody>
                    </table>
                </div>

            </div>
        </div>
    );
    */
}

export default TestPage;

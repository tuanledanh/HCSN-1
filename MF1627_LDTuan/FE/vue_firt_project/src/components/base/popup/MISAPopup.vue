<template>
    <div class="popup">
        <div class="popup-container">
            <!-- Popup head start-->
            <div class="popup-head">
                <span>Sửa tài sản</span>
                <div class="icon--exit"></div>
            </div>
            <!-- Popup head end-->

            <!-- Popup body start-->
            <div class="popup-body">
                <div class="popup-body__row">
                    <div class="popup__column">
                        <div class="label">
                            <label for="">Mã tài sản</label>
                            <div class="icon--require"></div>
                        </div>
                        <div class="input input--popup input--popup-width">
                            <input class="input__font--italic" type="text" placeholder="Nhập mã tài sản">
                        </div>
                    </div>
                    <div class="popup__column">
                        <div class="label">
                            <label for="">Tên tài sản</label>
                            <div class="icon--require"></div>
                        </div>
                        <div class="input input--popup input--popup-high-width">
                            <input class="input__font--italic" type="text" placeholder="Nhập tên tài sản">
                        </div>
                    </div>
                </div>
                <div class="popup-body__row">
                    <div class="popup__column">
                        <div class="label">
                            <label for="">Mã bộ phận sử dụng</label>
                            <div class="icon--require"></div>
                        </div>
                        <div class="input input--popup input--popup-width">
                            <input class="input__font--italic" type="text" placeholder="Chọn mã bộ phận sử dụng">
                            <div class="icon--input icon--input-right">
                                <div class="icon--drop-down"></div>
                            </div>
                        </div>
                    </div>
                    <div class="popup__column">
                        <div class="label">
                            <label for="">Tên bộ phận sử dụng</label>
                        </div>
                        <div class="input input--popup input--popup-high-width input--disabled">
                            <input class="input__font--italic" type="text" placeholder="" disabled>
                        </div>
                    </div>
                </div>
                <div class="popup-body__row">
                    <div class="popup__column">
                        <div class="label">
                            <label for="">Mã loại tài sản</label>
                            <div class="icon--require"></div>
                        </div>
                        <div class="input input--popup input--popup-width">
                            <input class="input__font--italic" type="text" placeholder="Chọn mã loại tài sản">
                            <div class="icon--input icon--input-right">
                                <div class="icon--drop-down"></div>
                            </div>
                        </div>
                    </div>
                    <div class="popup__column">
                        <div class="label">
                            <label for="">Tên loại tài sản</label>
                        </div>
                        <div class="input input--popup input--popup-high-width input--disabled">
                            <input class="input__font--italic" type="text" placeholder="" disabled>
                        </div>
                    </div>
                </div>
                <div class="popup-body__row">
                    <div class="popup__column">
                        <div class="label">
                            <label for="">Số lượng</label>
                            <div class="icon--require"></div>
                        </div>
                        <div class="input input--popup input--popup-width input__text--right input__text--right-padding">
                            <input class="" type="text" value="01">
                            <div class="icon--input icon--input-right icon--add-sub">
                                <div class="icon--pull-up"></div>
                                <div class="icon--drop-down"></div>
                            </div>
                        </div>
                    </div>
                    <div class="popup__column">
                        <div class="label">
                            <label for="">Nguyên giá</label>
                            <div class="icon--require"></div>
                        </div>
                        <div class="input input--popup input--popup-width input__text--right">
                            <input class="" type="text" value="0">
                        </div>
                    </div>
                    <div class="popup__column">
                        <div class="label">
                            <label for="">Số năm sử dụng</label>
                            <div class="icon--require"></div>
                        </div>
                        <div class="input input--popup input--popup-width input__text--right">
                            <input class="" type="text" value="0">
                        </div>
                    </div>
                </div>
                <div class="popup-body__row">
                    <div class="popup__column">
                        <div class="label">
                            <label for="">Tỷ lệ hao mòn (%)</label>
                            <div class="icon--require"></div>
                        </div>
                        <div class="input input--popup input--popup-width input__text--right input__text--right-padding">
                            <input class="" type="text" value="0">
                            <div class="icon--input icon--input-right icon--add-sub">
                                <div class="icon--pull-up"></div>
                                <div class="icon--drop-down"></div>
                            </div>
                        </div>
                    </div>
                    <div class="popup__column">
                        <div class="label">
                            <label for="">Giá trị hao mòn năm</label>
                            <div class="icon--require"></div>
                        </div>
                        <div class="input input--popup input--popup-width input__text--right">
                            <input class="" type="text" value="0">
                        </div>
                    </div>
                    <div class="popup__column">
                        <div class="label">
                            <label for="">Năm theo dõi</label>
                        </div>
                        <div class="input input--popup input--popup-width input__text--right input--disabled">
                            <input class="" type="text" value="2021" disabled>
                        </div>
                    </div>
                </div>
                <div class="popup-body__row">
                    <div class="popup__column">
                        <div class="label">
                            <label for="">Ngày mua</label>
                            <div class="icon--require"></div>
                        </div>
                        <div class="input input--popup input--popup-width">
                            <input class="" type="text" value="25/08/2021">
                            <div class="icon--input icon--input-right">
                                <div class="icon--date-picker"></div>
                            </div>
                        </div>
                    </div>
                    <div class="popup__column">
                        <div class="label">
                            <label for="">Ngày bắt đầu sử dụng</label>
                            <div class="icon--require"></div>
                        </div>
                        <div class="input input--popup input--popup-width">
                            <input class="" type="text" value="25/08/2021">
                            <div class="icon--input icon--input-right">
                                <div class="icon--date-picker"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Popup body end-->

            <!-- Popup foot start-->
            <div class="popup-foot">
                <button class="button button--main">Lưu</button>
                <button class="button button--sub">Hủy</button>
            </div>
            <!-- Popup foot end-->
        </div>
    </div>
</template>

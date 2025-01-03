document.getElementById("submitButton").addEventListener("click", async function () {
    const form = document.getElementById("registerForm");
    const formData = new FormData(form);

    // Xóa thông báo lỗi cũ
    document.getElementById("errorMessages").innerHTML = "";

    const response = await fetch("/Account/Register", {
        method: "POST",
        body: formData,
    });

    if (response.ok) {
        const result = await response.json();

        if (result.IsSuccess) {
            // Hiển thị thông báo thành công
            document.getElementById("registerPopup").style.display = "none";
            document.getElementById("overlay").style.display = "none";
            document.getElementById("successMessage").style.display = "block";

            // Ẩn thông báo sau 3 giây
            setTimeout(() => {
                document.getElementById("successMessage").style.display = "none";
            }, 3000);
        } else {
            // Hiển thị lỗi từ server
            const errorMessages = result.Errors.map(error => `<p>${error}</p>`).join("");
            document.getElementById("errorMessages").innerHTML = errorMessages;
        }
    } else {
        // Hiển thị lỗi hệ thống nếu có
        const result = await response.json();
        document.getElementById("errorMessages").innerHTML = `<p>${result.Message || "An error occurred."}</p>`;
    }
});
require(graphics)

c3 <- read.table("./data/c3.txt", header=TRUE)
p4 <- read.table("./data/p4.txt", header=TRUE)

data.1 <- data.frame(c(c3[1:11443,1:2], c3[1:11443,4:5]), p4[1:11443,3:6])

data.2 <- data.frame(c(data.1["date"], data.1["moment"], data.1["d"]*10 + data.1["u"], data.1["d1"]*10 + data.1["u1"], data.1["d2"]*10 + data.1["u2"]))
colnames(data.2) <- c("date", "moment", "f", "c1", "c2")
